package com.technoio.models;


import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.ForeignKey;
import androidx.room.Index;
import androidx.room.PrimaryKey;

import retrofit2.http.FormUrlEncoded;

@Entity(tableName = "order_items",
    foreignKeys = @ForeignKey(entity = Order.class, parentColumns = "id",
    childColumns = "order_id", onDelete = ForeignKey.CASCADE))

public class OrderItems {

    @PrimaryKey(autoGenerate = true)
    public long id;

    @ColumnInfo
    public int quantity;

    @ColumnInfo
    public long order_id;

    @ColumnInfo
    public String produit_nom;

    @ColumnInfo
    public double list_prix;

    @ColumnInfo
    public int discount;

    @ColumnInfo
    public String produit_photo;


    public OrderItems(int quantity, long order_id, String produit_nom, double list_prix, int discount, String produit_photo) {
        this.quantity = quantity;
        this.order_id = order_id;
        this.produit_nom = produit_nom;
        this.list_prix = list_prix;
        this.discount = discount;
        this.produit_photo = produit_photo;
    }
}

