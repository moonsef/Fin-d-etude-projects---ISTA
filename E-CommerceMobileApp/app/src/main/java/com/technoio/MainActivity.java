package com.technoio;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import androidx.room.Room;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Build;
import android.os.Bundle;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.technoio.adapters.ProduitCardAdapter;
import com.technoio.api.CategorieAPI;
import com.technoio.api.ProduitAPI;
import com.technoio.models.Categorie;
import com.technoio.models.Produit;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;

public class MainActivity extends AppCompatActivity {

    LinearLayout linearLayout;
    List<Button> buttons = new ArrayList<Button>();
    Retrofit retrofit;
    ProgressBar progressBar;
    RecyclerView recycleview;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        init();

    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.menu_item:
                logoutClient();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    private void logoutClient() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);

        builder.setMessage("Êtes-vous sûr de vouloir vous déconnecter ?")
            .setTitle("Alerte")
            .setPositiveButton("Oui", (dialog, i) -> {
                SharedPreferences pref = getSharedPreferences(getString(R.string.preference_file_key), 0);
                pref.edit().clear().commit();
                dialog.cancel();
                finish();
                startActivity(getIntent());
            })
            .setNegativeButton("Non", (dialog, i) -> {
                dialog.cancel();
            });

        builder.create().show();

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        SharedPreferences pref = getSharedPreferences(getString(R.string.preference_file_key), 0);
        String uuid = pref.getString("uuid", "");
        if(!uuid.isEmpty()){
            getMenuInflater().inflate(R.menu.main_menu, menu);
        }
        return super.onCreateOptionsMenu(menu);
    }

    private void init() {
        try {
            progressBar = findViewById(R.id.progressBar);
            linearLayout = findViewById(R.id.buttons_layout);
            recycleview = findViewById(R.id.recycleview);

            recycleview.setLayoutManager(new LinearLayoutManager(this));
            recycleview.addItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.VERTICAL));

            retrofit = APIClient.getClient();


            loadCategories();


        } catch (Exception ex) {
            Toast.makeText(this, ex.getMessage(), Toast.LENGTH_LONG).show();
        }
    }

    public void panierButtonClicked(View v){
        Intent intent = new Intent(this, PanierActivity.class);
        startActivity(intent);
    }

    private void loadCategories() {
        CategorieAPI cate = retrofit.create(CategorieAPI.class);
        Call<List<Categorie>> call = cate.getCategories();
        call.enqueue(new Callback<List<Categorie>>() {
            @Override
            public void onResponse(Call<List<Categorie>> call, Response<List<Categorie>> response) {
                if (response.isSuccessful()) {
                    List<Categorie> categories = response.body();
                    categories.add(0, new Categorie(0, "All"));
                    addCategoriesToScrollableMenu(categories);
                }
            }
            @Override
            public void onFailure(Call<List<Categorie>> call, Throwable t) {
                Toast.makeText(MainActivity.this, t.getMessage(), Toast.LENGTH_LONG).show();
                progressBar.setVisibility(View.GONE);
                call.cancel();
            }
        });
    }

    private void addCategoriesToScrollableMenu(List<Categorie> categories) {

        for (Categorie cate : categories) {
            Button btn = new Button(this);
            LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WRAP_CONTENT, 65);
            params.setMargins(0, 0, 12, 0);
            btn.setLayoutParams(params);
            btn.setTextColor(getResources().getColor(R.color.white));
            btn.setTag(cate.Nom);

            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O)  btn.setText(String.join(" ", cate.Nom.split("-")));
            else btn.setText(cate.Nom);

            btn.setOnClickListener(view -> onCategorieClicked(view));
            btn.setGravity(Gravity.CENTER);
            btn.setPadding(10, 0, 10, 0);
            btn.setBackground(getResources().getDrawable(R.drawable.rounded_view));
            btn.setTextSize(12);
            linearLayout.addView(btn);
        }

        loadLastestProducts();
    }

    private void loadLastestProducts(){
        Call<List<Produit>> call = retrofit.create(ProduitAPI.class).getAllProduits();
        call.enqueue(new Callback<List<Produit>>() {
            @Override
            public void onResponse(Call<List<Produit>> call, Response<List<Produit>> response) {
                if(response.isSuccessful()){
                    ProduitCardAdapter adapter = new ProduitCardAdapter(response.body());
                    recycleview.setAdapter(adapter);

                }
                progressBar.setVisibility(View.GONE);
            }

            @Override
            public void onFailure(Call<List<Produit>> call, Throwable t) {
                Toast.makeText(MainActivity.this, t.getMessage(), Toast.LENGTH_LONG).show();
                progressBar.setVisibility(View.GONE);
                call.cancel();
            }
        });
    }



    private void onCategorieClicked(View v) {
        for (Button b: buttons) {
            b.setBackground(getResources().getDrawable(R.drawable.rounded_view));
        }
        recycleview.setAdapter(null);
        progressBar.setVisibility(View.VISIBLE);

        Button btn = (Button) v;
        btn.setBackground(getResources().getDrawable(R.drawable.rounded_view_clicked));
        if(btn.getTag().toString() == "All"){
            loadLastestProducts();
            buttons.add((Button) v);
            return;
        }
        Call<List<Produit>> call = retrofit.create(ProduitAPI.class).getProduitsByCategorie(btn.getTag().toString());
        call.enqueue(new Callback<List<Produit>>() {
            @Override
            public void onResponse(Call<List<Produit>> call, Response<List<Produit>> response) {
                if (response.isSuccessful()) {
                    ProduitCardAdapter adapter = new ProduitCardAdapter(response.body());
                    recycleview.setAdapter(adapter);
                }
                progressBar.setVisibility(View.GONE);

            }

            @Override
            public void onFailure(Call<List<Produit>> call, Throwable t) {
                Toast.makeText(MainActivity.this, t.getMessage(), Toast.LENGTH_LONG).show();
                progressBar.setVisibility(View.GONE);
                call.cancel();
            }
        });

        buttons.add((Button) v);
    }
}
