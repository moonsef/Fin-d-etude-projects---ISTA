package com.technoio.adapters;

import android.content.Intent;
import android.graphics.Paint;
import android.util.Base64;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.technoio.ProduitActivity;
import com.technoio.R;
import com.technoio.models.Produit;

import java.util.List;

public class ProduitCardAdapter extends RecyclerView.Adapter<ProduitCardAdapter.ViewHolder> {
    private List<Produit> produits;

    public ProduitCardAdapter(List<Produit> produits){
        this.produits = produits;
    }

    @Override
    public ProduitCardAdapter.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View rowItem = LayoutInflater.from(parent.getContext()).inflate(R.layout.produit_card, parent, false);
        return new ViewHolder(rowItem);
    }

    @Override
    public void onBindViewHolder(ViewHolder holder, int position) {
        Produit produit = produits.get(position);
        holder.txtNom.setText(produit.Nom);
        holder.txtDesc.setText(produit.Description);

        if(produit.Quantity > 0){
            holder.txtStatut.setText(String.format("Produit en stock (%s)", produit.Quantity));
            holder.txtStatut.setTextColor(holder.itemView.getResources().getColor(R.color.green_400));

        }else{
            holder.txtStatut.setText("Produit rupture de stock");
            holder.txtStatut.setTextColor(holder.itemView.getResources().getColor(R.color.purple_700));
        }


        if(produit.Promo == 0){
           holder.txtPrixRedu.setVisibility(View.GONE);
           holder.txtPrixReal.setText(produit.Prix + ",00 MAD");
        }else{
            int prix = (Integer.parseInt(produit.Prix) - (produit.Promo * Integer.parseInt(produit.Prix) / 100));
            holder.txtPrixRedu.setText(produit.Prix + ",00 MAD");
            holder.txtPrixRedu.setPaintFlags(holder.txtPrixRedu.getPaintFlags() | Paint.STRIKE_THRU_TEXT_FLAG);
            holder.txtPrixReal.setText(prix + ",00 MAD");
        }

        Glide.with(holder.image.getContext())
            .asBitmap()
            .load(Base64.decode(produit.Photos.get(0).Photo.getBytes(), Base64.DEFAULT))
            .into(holder.image);


        if(holder.stars_layout.getChildCount() < 5){
            for (int i = 0; i < produit.Rating; i++){
                ImageView star = setupStarIcon(holder);
                star.setBackground(holder.stars_layout.getResources().getDrawable(R.drawable.ic_baseline_star_24));
                holder.stars_layout.addView(star);
            }


            for(int i = produit.Rating; i < 5; i++){
                ImageView star = setupStarIcon(holder);
                star.setBackground(holder.stars_layout.getResources().getDrawable(R.drawable.ic_baseline_star_24_black));
                holder.stars_layout.addView(star);
            }
        }

        holder.txtAvis.setText(String.format("Avis (%s)", produit.TotalRating));
    }

    private ImageView setupStarIcon(ViewHolder holder){
        ImageView star = new ImageView(holder.stars_layout.getContext());
        LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(40, 40);
        params.setMargins(0, 0,3,0);
        star.setLayoutParams(params);
        return star;
    }


    @Override
    public int getItemCount() {
        return produits.size();
    }

    public static class ViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener {
        private TextView txtNom;
        private TextView txtDesc;
        private TextView txtStatut;
        private TextView txtAvis;
        private TextView txtPrixReal;
        private TextView txtPrixRedu;
        private ImageView image;
        private LinearLayout stars_layout;




        public ViewHolder(View view) {
            super(view);
            view.setOnClickListener(this);
            txtNom = view.findViewById(R.id.name);
            txtDesc = view.findViewById(R.id.desc);
            txtStatut = view.findViewById(R.id.statut);
            txtAvis = view.findViewById(R.id.avis);
            txtPrixReal = view.findViewById(R.id.prix_real);
            txtPrixRedu = view.findViewById(R.id.prix_redu);
            image = view.findViewById(R.id.imageview);
            stars_layout = view.findViewById(R.id.stars_layout);

        }

        @Override
        public void onClick(View view) {
            Intent intent = new Intent(view.getContext(), ProduitActivity.class);
            intent.putExtra("name", txtNom.getText().toString());
            view.getContext().startActivity(intent);
        }
    }
}
