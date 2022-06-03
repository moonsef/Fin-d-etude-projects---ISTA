package com.example.chatappofpptdefindtude;

import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;

import com.github.javafaker.Faker;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.storage.FirebaseStorage;
import java.util.HashMap;
import java.util.Map;
import java.util.Random;


public class ProfileActivity extends AppCompatActivity {

    private EditText tConfrimPass;
    private EditText tFullname;
    private EditText tPassword;
    private FirebaseFirestore db;
    private FirebaseAuth mAuth;
    private FirebaseStorage storage;
    private ImageView imageView;
    private ProgressBar progressBar;
    private Button button;

    Map<String, Object> user = new HashMap<>();


    private void init(){
        try{

            db = FirebaseFirestore.getInstance();
            mAuth = FirebaseAuth.getInstance();
            storage = FirebaseStorage.getInstance();
            tFullname = findViewById(R.id.fullnameTextProfile);
            tPassword = findViewById(R.id.passwordTextProfile);
            tConfrimPass = findViewById(R.id.confirmpasswordTextProfile);
            imageView = findViewById(R.id.profileImage);
            progressBar = findViewById(R.id.progressBar);
            button = findViewById(R.id.button2);


            if(progressBar.getVisibility() == View.VISIBLE){
                button.setEnabled(false);
            }

        }catch (Exception e){
            Toast.makeText(this, e.getMessage(),Toast.LENGTH_LONG ).show();
        }
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);
        init();
        getUserDataAndDisplayIt();

    }


    private void getUserDataAndDisplayIt(){
        try{
            db.collection("users").document(mAuth.getUid()).get().addOnCompleteListener(task -> {
                if (task.isSuccessful()) {
                    DocumentSnapshot user = task.getResult();

                    if(user.exists()){
                        tFullname.setText(user.get("fullname").toString());
                        Utils.fetchSvg(this, user.get("avatar").toString(), imageView);
                    }
                } else {
                    Toast.makeText(this, task.getException().getMessage(), Toast.LENGTH_LONG).show();
                }
                button.setEnabled(true);
                progressBar.setVisibility(View.INVISIBLE);
            });

        }catch(Exception e){
            Toast.makeText(this, e.getMessage(),Toast.LENGTH_LONG ).show();
            progressBar.setVisibility(View.INVISIBLE);
        }
    }

    public void updateUser(View v){

        user.put("fullname", tFullname.getText().toString());
        try{

            if(!TextUtils.isEmpty(tPassword.getText().toString())){
                if(!tPassword.getText().toString().equals(tConfrimPass.getText().toString())){
                    Toast.makeText(this, "Confirmer le mot de passe incorrect", Toast.LENGTH_LONG).show();
                }else{
                    mAuth.getCurrentUser().updatePassword(tPassword.getText().toString());
                    db.collection("users").document(mAuth.getUid()).update(user);
                    Toast.makeText(this, "Mise à jour du profil réussie", Toast.LENGTH_LONG).show();

                }
            }else{
                db.collection("users").document(mAuth.getUid()).update(user);
                Toast.makeText(this, "Mise à jour du profil réussie", Toast.LENGTH_LONG).show();
            }



        }catch (Exception e){
            Toast.makeText(this, e.getMessage(), Toast.LENGTH_LONG).show();
        }
    }

    public void changeProfileImage(View v){
        Faker faker = new Faker();
        String urlImage = String.format("https://avatars.dicebear.com/api/male/%s.svg?background=%s", faker.name().firstName(),randomColorHex());
        Utils.fetchSvg(this, urlImage, imageView);
        user.put("avatar", urlImage);
    }

    private String randomColorHex(){
        Random random = new Random();
        int nextInt = random.nextInt(0xffffff + 1);
        return "%23" + String.format("%06x", nextInt);
    }

    public void backToMainActivity(View v){
        finish();
    }
}
