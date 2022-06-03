package com.technoio.adapters;

import android.content.Context;
import android.util.Base64;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.technoio.AppDatabase;
import com.technoio.PanierActivity;
import com.technoio.R;
import com.technoio.models.OrderAndOrderItem;

import java.util.List;

public class ProduitPanierAdapter extends RecyclerView.Adapter<ProduitPanierAdapter.ViewHolder> {

    private List<OrderAndOrderItem> orderAndOrderItemList;
    private AppDatabase app;
    private Context context;
    public ProduitPanierAdapter(List<OrderAndOrderItem> orderAndOrderItemList, AppDatabase app, Context context) {
        this.orderAndOrderItemList = orderAndOrderItemList;
        this.app = app;
        this.context = context;
    }

    @Override
    public ProduitPanierAdapter.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View rowItem = LayoutInflater.from(parent.getContext()).inflate(R.layout.produit_panier, parent, false);
        return new ProduitPanierAdapter.ViewHolder(rowItem);
    }

    @Override
    public void onBindViewHolder(ProduitPanierAdapter.ViewHolder holder, int position) {
        OrderAndOrderItem item = orderAndOrderItemList.get(position);

        holder.nomTextView.setText(String.format("%s (x%s)", item.orderItems.produit_nom, item.orderItems.quantity));
        int prix_apres_redu = (int) (item.orderItems.list_prix - (item.orderItems.list_prix * item.orderItems.discount / 100));
        holder.prixTextView.setText(prix_apres_redu * item.orderItems.quantity + ", 00 MAD");

        Glide.with(holder.imageView.getContext())
            .asBitmap()
            .load(Base64.decode(item.orderItems.produit_photo.getBytes(), Base64.DEFAULT))
            .into(holder.imageView);

        holder.deleteIcon.setOnClickListener(view -> {
            app.orderDoa().deleteById(item.order.id);
            PanierActivity activity  = (PanierActivity)context;
            activity.finish();
            view.getContext().startActivity(activity.getIntent());
            Toast.makeText(view.getContext(), "Order a été supprimer",Toast.LENGTH_LONG).show();
        });


    }



    @Override
    public int getItemCount() {
        return orderAndOrderItemList.size();
    }

    public static class ViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener {
        private ImageView imageView;
        private TextView nomTextView;
        private TextView prixTextView;
        private ImageView deleteIcon;


        public ViewHolder(View view) {
            super(view);
            view.setOnClickListener(this);
            imageView = view.findViewById(R.id.imageview);
            nomTextView = view.findViewById(R.id.name);
            prixTextView = view.findViewById(R.id.prix);
            deleteIcon = view.findViewById(R.id.deleteIcon);

        }


        @Override
        public void onClick(View view) {

        }
    }
}
