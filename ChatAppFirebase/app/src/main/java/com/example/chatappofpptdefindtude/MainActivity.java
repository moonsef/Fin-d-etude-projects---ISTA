package com.example.chatappofpptdefindtude;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.DialogFragment;

import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.view.View;
import android.widget.PopupMenu;
import android.widget.Toast;

import com.google.firebase.auth.FirebaseAuth;

public class MainActivity extends AppCompatActivity {

    private FirebaseAuth mAuth;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mAuth = FirebaseAuth.getInstance();
    }



    @Override
    public void onBackPressed() {
        finishAffinity();
    }

    @RequiresApi(api = Build.VERSION_CODES.Q)
    public void openMenuPopUp(View v){

        PopupMenu popup = new PopupMenu(MainActivity.this,v);
        popup.getMenuInflater().inflate(R.menu.popup_menu, popup.getMenu());
        popup.setForceShowIcon(true);
        popup.setOnMenuItemClickListener(menuItem -> {
            switch (menuItem.getTitle().toString()){
                case "Profil":
                    startActivity(new Intent(this, ProfileActivity.class));
                    break;
                case "Se déconnecter":
                    logOut();
                 break;
            }

            return true;
        });

        popup.show();
    }

    private void logOut(){
        runOnUiThread(new AlertDialog.Builder(MainActivity.this)
            .setTitle("Sortir")
            .setMessage("Vous voulez vous déconnecter ?")
            .setCancelable(true)
            .setPositiveButton("Oui", (dialogInterface, i) -> {
                dialogInterface.cancel();
                mAuth.signOut();
                startActivity(new Intent(getApplicationContext(), AuthActivity.class));
            })
            .setNegativeButton("Non", ((dialogInterface, i) -> {
                dialogInterface.cancel();
            }))::show);
    }
}

