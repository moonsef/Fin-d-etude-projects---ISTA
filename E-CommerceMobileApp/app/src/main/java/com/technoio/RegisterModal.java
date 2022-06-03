package com.technoio;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;

import com.technoio.api.ClientAPI;

import java.io.IOException;
import java.util.HashMap;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;

public class RegisterModal extends DialogFragment {

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }


    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        LayoutInflater inflater = requireActivity().getLayoutInflater();


        View v = inflater.inflate(R.layout.register_form, null);


        builder.setView(v);

        AlertDialog dialog = builder.create();
        v.findViewById(R.id.cancel).setOnClickListener(l -> dialog.cancel());
        v.findViewById(R.id.login).setOnClickListener(l -> registerClient(dialog, v));

        v.findViewById(R.id.d).setOnClickListener(l -> {
            dialog.cancel();
            DialogFragment fragment = new LoginModal();
            fragment.show(getParentFragmentManager(), "modal2");
        });
        return dialog;
    }

    private void registerClient(AlertDialog dialog, View v) {
        EditText email = v.findViewById(R.id.email);
        EditText password  = v.findViewById(R.id.password);
        EditText tele  = v.findViewById(R.id.tele);
        EditText adresse  = v.findViewById(R.id.adresse);
        EditText nom  = v.findViewById(R.id.nom);
        EditText prenom  = v.findViewById(R.id.prenom);
        TextView error = v.findViewById(R.id.error_msg);
        Button login = v.findViewById(R.id.login);

        error.setText("");
        login.setText("Chargement...");

        HashMap<String , Object> user = new HashMap<>();
        user.put("email", email.getText().toString());
        user.put("password", password.getText().toString());
        user.put("telephone", tele.getText().toString());
        user.put("adresse", adresse.getText().toString());
        user.put("nom", nom.getText().toString());
        user.put("prenom", prenom.getText().toString());
        user.put("phone", true);

        try {

            Retrofit retrofit = APIClient.getClient();
            retrofit.create(ClientAPI.class).registerClient(user).enqueue(new Callback<String>() {
                @Override
                public void onResponse(Call<String> call, Response<String> response) {
                    if(response.isSuccessful()){
                        SharedPreferences pref =  getActivity().getSharedPreferences(getString(R.string.preference_file_key), Context.MODE_PRIVATE);
                        SharedPreferences.Editor editor = pref.edit();
                        editor.putString("uuid", response.body());
                        editor.commit();
                        dialog.cancel();
                    }else if(response.code() == 400){
                        try {
                            error.setText(response.errorBody().string());
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                    login.setText("Register");
                }

                @Override
                public void onFailure(Call<String> call, Throwable t) {
                    Toast.makeText(v.getContext(), t.getMessage(), Toast.LENGTH_LONG).show();
                    login.setText("Register");
                    call.cancel();
                }
            });
        }catch (Exception ex){
            Toast.makeText(v.getContext(), ex.getMessage(), Toast.LENGTH_LONG).show();
            login.setText("Register");

        }

    }
}
