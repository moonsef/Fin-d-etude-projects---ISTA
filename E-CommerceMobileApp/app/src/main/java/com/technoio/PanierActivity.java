package com.technoio;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.DialogFragment;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import androidx.room.Room;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

import com.technoio.adapters.ProduitPanierAdapter;
import com.technoio.models.OrderAndOrderItem;

import java.util.List;

public class PanierActivity extends AppCompatActivity {

    AppDatabase db;
    RecyclerView recyclerView;


    @Override
    public void onBackPressed() {
    }

    @Override
    protected void onStart() {
        super.onStart();
        SharedPreferences pref =  getSharedPreferences(getString(R.string.preference_file_key), Context.MODE_PRIVATE);
        String uuid = pref.getString("uuid", "");
        if(uuid.isEmpty()){
            findViewById(R.id.floatingActionButton).setVisibility(View.GONE);
            Toast.makeText(this, "Please authentifié toi", Toast.LENGTH_LONG).show();
            DialogFragment fragment = new RegisterModal();
            fragment.show(getSupportFragmentManager(),"modal3");
            return;
        }

        List<OrderAndOrderItem> list = db.orderDoa().getOrdersAndOrderItems(uuid);


        ProduitPanierAdapter adapter = new ProduitPanierAdapter(list, db, this);
        recyclerView.setAdapter(adapter);
    }

    public void payButtonClicked(View v){
        if(recyclerView.getChildCount() ==  0) {
            Toast.makeText(this, "Le panier est vide!", Toast.LENGTH_LONG).show();
            return;
        }
        SharedPreferences pref =  getSharedPreferences(getString(R.string.preference_file_key), Context.MODE_PRIVATE);
        String uuid = pref.getString("uuid", "");

        DialogFragment fragment = new PaimentModal(db, uuid , this);
        fragment.show(getSupportFragmentManager(),"modal4");
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_panier);

        recyclerView = findViewById(R.id.recycleview1);

        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        recyclerView.addItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.VERTICAL));

        try{

            db = Room.databaseBuilder(getApplicationContext(),
                AppDatabase.class, "db")
                .allowMainThreadQueries()
                .build();

        }catch (Exception ex){
            Toast.makeText(this, ex.getMessage(), Toast.LENGTH_LONG).show();
        }

    }
}
