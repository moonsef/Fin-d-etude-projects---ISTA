package com.technoio.api;

import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;

import com.technoio.models.OrderItems;


@Dao
public interface OrderItemsDoa {

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    void insertOrderItem(OrderItems orderitem);
}
