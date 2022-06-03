package com.example.chatappofpptdefindtude;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.SetOptions;

import java.util.HashMap;
import java.util.Map;
import java.util.Random;
import java.util.regex.Pattern;

public class AuthActivity extends AppCompatActivity {

    private FirebaseAuth mAuth;
    private FirebaseFirestore db;
    private EditText tEmail;
    private EditText tPassword;
    private EditText tUsername;
    private EditText tFullname;
    private TextView tError;
    private ProgressBar progressBar;
    private boolean isRegistration = true;

    @Override
    protected void onStart() {
        super.onStart();
        FirebaseUser currentUser = mAuth.getCurrentUser();
        if(currentUser != null){
            currentUser.reload().addOnCompleteListener(task -> {
                if(task.isSuccessful()){
                    openNewActivity();
                }
                progressBar.setVisibility(View.INVISIBLE);
            });
        }else{
            progressBar.setVisibility(View.INVISIBLE);
        }
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        init();

    }

    private void init(){
        try{
            db = FirebaseFirestore.getInstance();
            mAuth = FirebaseAuth.getInstance();
            tEmail = findViewById(R.id.emailTextProfile);
            tPassword = findViewById(R.id.passwordText);
            tError = findViewById(R.id.textView2);
            progressBar = findViewById(R.id.progressBar);
            tUsername = findViewById(R.id.usernameText);
            tFullname = findViewById(R.id.fullnameText);

        }catch(Exception e){
            Toast.makeText(this, e.getMessage(),Toast.LENGTH_LONG ).show();
        }
    }

    @Override
    public void onBackPressed() {
        finishAffinity();
    }


    public void authenticateUser(View v){


        if(isRegistration){
            if(!Pattern.matches("^([a-z0-9_.]){5,30}$", tUsername.getText().toString())){
                showErrorInUI("Invalid 'Nom d'utilisateur'");
                return;
            }

            if(!Pattern.matches("(^[A-Za-z]{3,16})([ ]?)([A-Za-z]{3,16})?([ ]?)?([A-Za-z]{3,16})?([ ]?)?([A-Za-z]{3,16})", tFullname.getText().toString())){
                showErrorInUI("Invalid 'Nom et prénom'");
                return;
            }
        }


        Map<String, String> user = new HashMap<>();
        user.put("username", tUsername.getText().toString());
        user.put("fullname", tFullname.getText().toString());
        user.put("email", tEmail.getText().toString());
        user.put("avatar", String.format("https://avatars.dicebear.com/api/male/%s.svg?background=%s", tUsername.getText().toString(),randomColorHex()));


        try{

            progressBar.setVisibility(View.VISIBLE);
            tError.setVisibility(View.INVISIBLE);
            tError.setText("");


            if(isRegistration){

                db.collection("users").whereEqualTo("username", user.get("username")).get().addOnCompleteListener(task -> {
                    if(task.isSuccessful()){
                            if(!task.getResult().isEmpty()){
                                showErrorInUI("Le nom d'utilisateur existe déjà");
                            }else{
                                mAuth.createUserWithEmailAndPassword(tEmail.getText().toString(), tPassword.getText().toString()).addOnCompleteListener(this, task1 -> {
                                    if(task1.isSuccessful()){
                                        db.collection("users")
                                            .document(mAuth.getCurrentUser().getUid())
                                            .set(user, SetOptions.merge())
                                            .addOnCompleteListener(task2 -> {
                                               if(task2.isSuccessful()){
                                                    openNewActivity();
                                               }else{
                                                   showErrorInUI(task2.getException().getMessage());
                                               }
                                                progressBar.setVisibility(View.INVISIBLE);
                                        });

                                    }else{
                                        showErrorInUI(task1.getException().getMessage());
                                    }
                                    progressBar.setVisibility(View.INVISIBLE);

                                });

                            }

                    }else{
                        showErrorInUI(task.getException().getMessage());
                    }
                    progressBar.setVisibility(View.INVISIBLE);
                });
            }else{
                mAuth.signInWithEmailAndPassword(tEmail.getText().toString(), tPassword.getText().toString()).addOnCompleteListener(this, task1 -> {

                    if(task1.isSuccessful()){
                        openNewActivity();
                    }else{
                        showErrorInUI(task1.getException().getMessage());
                    }
                    progressBar.setVisibility(View.INVISIBLE);
                });
            }

        }catch(Exception e){
            Toast.makeText(this, e.getMessage(), Toast.LENGTH_LONG).show();
            progressBar.setVisibility(View.INVISIBLE);
        }
    }

    private void openNewActivity() {
        Intent intent = new Intent(this, MainActivity.class);
        startActivity(intent);
    }

    private void showErrorInUI(String msg){

        tError.setVisibility(View.VISIBLE);
        tError.setText(msg);
    }

    public void onAuthScreenStateChange(View v){
        isRegistration = !isRegistration;

        if(!isRegistration){
            tUsername.setVisibility(View.GONE);
            tFullname.setVisibility(View.GONE);
            ((Button) findViewById(R.id.button)).setText("Connexion");
            ((TextView) findViewById(R.id.textView4)).setText(R.string.dont_account);
        }else{
            ((Button) findViewById(R.id.button)).setText("Inscription");
            ((TextView) findViewById(R.id.textView4)).setText(R.string.had_account);
            tUsername.setVisibility(View.VISIBLE);
            tFullname.setVisibility(View.VISIBLE);
        }

    }
    private String randomColorHex(){
        Random random = new Random();

        int nextInt = random.nextInt(0xffffff + 1);

        return "%23" + String.format("%06x", nextInt);
    }
}
