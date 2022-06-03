package com.technoio;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;

import com.technoio.api.ClientAPI;
import com.technoio.api.OrderDoa;
import com.technoio.models.Order;
import com.technoio.models.OrderAndOrderItem;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;

public class PaimentModal extends DialogFragment {

    private AppDatabase db;
    private String uuid;
    private  Context context;


    public PaimentModal(AppDatabase db, String uuid, Context context){
        this.db = db;
        this.uuid =uuid;
        this.context = context;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }


    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        LayoutInflater inflater = requireActivity().getLayoutInflater();


        View v = inflater.inflate(R.layout.paiment_form, null);


        builder.setView(v);

        AlertDialog dialog = builder.create();
        v.findViewById(R.id.cancel).setOnClickListener(l -> dialog.cancel());
        v.findViewById(R.id.payer).setOnClickListener(l -> payerClicked(dialog, v));
        TextView amount_total = v.findViewById(R.id.amount_total);

        int totalCommandPrix = db.orderDoa().getTotalPrixCommand(uuid);
        amount_total.setText("Montant payable est: " + totalCommandPrix + ",00 MAD");

        return dialog;
    }

    private void payerClicked(AlertDialog dialog, View v) {

        TextView error = v.findViewById(R.id.error_msg);
        Button payer = v.findViewById(R.id.payer);

        error.setText("");
        payer.setText("Chargement...");

        List<HashMap<String , Object>> orders = new ArrayList<>();

        for (OrderAndOrderItem orderAndItem:  db.orderDoa().getOrdersAndOrderItems(uuid)) {
            HashMap<String, Object> order =  new HashMap<>();
            order.put("quantity", orderAndItem.orderItems.quantity);
            order.put("clientuuid", orderAndItem.order.client_uuid);
            order.put("produitname", orderAndItem.orderItems.produit_nom);
            orders.add(order);
        }


        try {

            Retrofit retrofit = APIClient.getClient();
            retrofit.create(ClientAPI.class).postOrder(orders).enqueue(new Callback<Void>() {
                @Override
                public void onResponse(Call<Void> call, Response<Void> response) {
                    if(response.isSuccessful()){
                        db.orderDoa().deleteAll();
                        dialog.cancel();

                        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());

                        builder.setMessage("Les produits sont payés avec succès. Veuillez vérifier votre adresse e-mail pour plus d'informations.")
                            .setTitle("Félicitation")
                            .setPositiveButton("Ok", (dialog , i) -> {
                                dialog.cancel();
                                PanierActivity activity = (PanierActivity)context;
                                activity.finish();
                                context.startActivity(activity.getIntent());
                            });

                        builder.create().show();
                    }else if(response.code() == 400){
                        try {
                            error.setText(response.errorBody().string());
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                    payer.setText("Payer");
                }

                @Override
                public void onFailure(Call<Void> call, Throwable t) {
                    Toast.makeText(v.getContext(), t.getMessage(), Toast.LENGTH_LONG).show();
                    Log.i("INFO", t.getMessage());

                    payer.setText("Payer");
                    call.cancel();
                }
            });
        }catch (Exception ex){
            Toast.makeText(v.getContext(), ex.getMessage(), Toast.LENGTH_LONG).show();
            Log.i("INFO", ex.getMessage());
            payer.setText("Payer");

        }

    }
}
