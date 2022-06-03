package com.technoio.models;


import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;
import androidx.room.TypeConverter;

import com.bumptech.glide.annotation.Excludes;

import java.sql.Date;

@Entity(tableName = "orders")
public class Order {

    @PrimaryKey(autoGenerate = true)
    public long id;

    @ColumnInfo()
    public String client_uuid;


    @ColumnInfo()
    public String order_date;

    public Order(String client_uuid,  String order_date) {
        this.client_uuid = client_uuid;
        this.order_date = order_date;
    }
}
