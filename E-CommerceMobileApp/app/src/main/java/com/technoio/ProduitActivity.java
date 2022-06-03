package com.technoio;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.DialogFragment;
import androidx.room.Room;

import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Paint;
import android.os.Bundle;
import android.util.Base64;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.bumptech.glide.Glide;
import com.technoio.api.ProduitAPI;
import com.technoio.models.Order;
import com.technoio.models.OrderItems;
import com.technoio.models.Photo;
import com.technoio.models.Produit;

import java.util.Date;
import java.util.HashMap;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;

public class ProduitActivity extends AppCompatActivity {

    private Retrofit retrofit;
    private ImageView image_hero;
    private ProgressBar progressBar;
    private LinearLayout other_image_container;
    private TextView txtNom;
    private TextView txtDesc;
    private TextView txtStatut;
    private TextView txtAvis;
    private TextView txtPrixReal;
    private TextView txtPrixRedu;
    private LinearLayout stars_layout;
    private ImageView marque_image;
    private ImageView add_image;
    private ImageView remove_image;
    private TextView quantity;
    private Button addToButton;
    private AppDatabase db;
    private Produit produit;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_produit);
        init();
    }

    private void init(){
        try{
            image_hero = findViewById(R.id.image_hero);
            progressBar = findViewById(R.id.progressbar);
            txtNom = findViewById(R.id.name);
            txtDesc = findViewById(R.id.desc);
            txtStatut = findViewById(R.id.statut);
            txtAvis = findViewById(R.id.avis);
            txtPrixReal = findViewById(R.id.prix_real);
            txtPrixRedu = findViewById(R.id.prix_redu);
            stars_layout = findViewById(R.id.stars_layout);
            marque_image = findViewById(R.id.marque_photo);
            remove_image = findViewById(R.id.remove);
            add_image = findViewById(R.id.addd);
            other_image_container = findViewById(R.id.other_images_container);
            quantity = findViewById(R.id.quantity);
            addToButton = findViewById(R.id.addToButton);




            add_image.setOnClickListener(c -> addQuantityButton());
            remove_image.setOnClickListener(c -> removeQuantityButton());

            db = Room.databaseBuilder(getApplicationContext(),
                AppDatabase.class, "db")
                .allowMainThreadQueries()
                .build();


            retrofit = APIClient.getClient();
            loadProduit();
        }catch (Exception ex){
            Toast.makeText(this, ex.getMessage(), Toast.LENGTH_LONG).show();
        }
    }



    public void addToCart(View v){

        if(produit != null){
            try{
                SharedPreferences pref =  getSharedPreferences(getString(R.string.preference_file_key), Context.MODE_PRIVATE);
                String uuid = pref.getString("uuid", "");
                if(uuid.isEmpty()){
                    DialogFragment fragment = new LoginModal();
                    fragment.show(getSupportFragmentManager(),"modal");
                    return;
                }

                int produit_quantity = Integer.parseInt(quantity.getText().toString());

                long order_id = db.orderDoa().insertOrder(new Order(uuid, new Date().toString()));

                db.OrderItemsDoa().insertOrderItem(new OrderItems(produit_quantity, order_id, produit.Nom, Double.parseDouble(produit.Prix), produit.Promo, produit.Photos.get(0).Photo));
                Toast.makeText(this, "Produit ajouté au panier avec succès", Toast.LENGTH_LONG).show();

            }catch (Exception ex){
                Toast.makeText(this, ex.getMessage(), Toast.LENGTH_LONG).show();
            }

        }

    }



    private void removeQuantityButton(){
        int oldvalue = Integer.parseInt(quantity.getText().toString());
        if(oldvalue != 1)
            quantity.setText(String.valueOf( oldvalue - 1));
    }

    private void addQuantityButton(){
        int oldvalue = Integer.parseInt(quantity.getText().toString());
        quantity.setText(String.valueOf( oldvalue+ 1));
    }

    @Override
    public void onBackPressed() { }

    private void loadProduit() {
        HashMap hash = new  HashMap<String, Object>();
        hash.put("Nom", getIntent().getStringExtra("name"));
        setTitle(getIntent().getStringExtra("name"));


        Call<Produit> call = retrofit.create(ProduitAPI.class).getProduitByName(hash);
        call.enqueue(new Callback<Produit>() {
            @Override
            public void onResponse(Call<Produit> call, Response<Produit> response) {
                if(response.isSuccessful()){
                    produit = response.body();
                    loadProduitDataToView(response.body());
                }

                Log.i("INFO", response.message());
                progressBar.setVisibility(View.GONE);
            }

            @Override
            public void onFailure(Call<Produit> call, Throwable t) {
                Toast.makeText(ProduitActivity.this, t.getMessage(), Toast.LENGTH_LONG).show();
                call.cancel();
                progressBar.setVisibility(View.GONE);
            }
        });
    }

    private void loadProduitDataToView(Produit produit) {
        Glide.with(this)
            .asBitmap()
            .load(Base64.decode(produit.Photos.get(0).Photo.getBytes(), Base64.DEFAULT))
            .into(image_hero);


        Glide.with(this)
            .asBitmap()
            .load(Base64.decode(produit.Marque.Photo.getBytes(), Base64.DEFAULT))
            .into(marque_image);


        txtNom.setText(produit.Nom);
        txtDesc.setText(produit.Description);

        if(produit.Quantity > 0){
            txtStatut.setText(String.format("Produit en stock (%s)", produit.Quantity));
            txtStatut.setTextColor(getResources().getColor(R.color.green_400));

        }else{
            txtStatut.setText("Produit rupture de stock");
            txtStatut.setTextColor(getResources().getColor(R.color.purple_700));
        }

        if(produit.Quantity == 0) addToButton.setEnabled(false);


        LoadPriceWithPromoLogic(produit);

        LoadImagesToContainer(produit);

        LoadStars(produit);

        txtAvis.setText(String.format("Avis (%s)", produit.TotalRating));


    }

    private void LoadPriceWithPromoLogic(Produit produit) {
        if(produit.Promo == 0){
            txtPrixRedu.setVisibility(View.GONE);
            txtPrixReal.setText(produit.Prix + ",00 MAD");
        }else{
            int prix = (Integer.parseInt(produit.Prix) - (produit.Promo * Integer.parseInt(produit.Prix) / 100));
            txtPrixRedu.setText(produit.Prix + ",00 MAD");
            txtPrixRedu.setPaintFlags(txtPrixRedu.getPaintFlags() | Paint.STRIKE_THRU_TEXT_FLAG);
            txtPrixReal.setText(prix + ",00 MAD");
        }
    }

    private void LoadStars(Produit produit) {
        if(stars_layout.getChildCount() < 5){
            for (int i = 0; i < produit.Rating; i++){
                ImageView star = setupStarIcon();
                star.setBackground(stars_layout.getResources().getDrawable(R.drawable.ic_baseline_star_24));
                stars_layout.addView(star);
            }


            for(int i = produit.Rating; i < 5; i++){
                ImageView star = setupStarIcon();
                star.setBackground(stars_layout.getResources().getDrawable(R.drawable.ic_baseline_star_24_black));
                stars_layout.addView(star);
            }
        }
    }

    private void LoadImagesToContainer(Produit produit) {
        for (Photo photo : produit.Photos) {
            ImageView img = new ImageView(this);
            LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(300, 300);
            img.setLayoutParams(params);

            Glide.with(this)
                .asBitmap()
                .load(Base64.decode(photo.Photo.getBytes(), Base64.DEFAULT))
                .into(img);

            img.setOnClickListener(l -> {
                Glide.with(this)
                    .asBitmap()
                    .load(Base64.decode(photo.Photo.getBytes(), Base64.DEFAULT))
                    .into(image_hero);
            });

            other_image_container.addView(img);
        }
    }

    private ImageView setupStarIcon(){
        ImageView star = new ImageView(this);
        LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(40, 40);
        params.setMargins(0, 0,3,0);
        star.setLayoutParams(params);
        return star;
    }
}
