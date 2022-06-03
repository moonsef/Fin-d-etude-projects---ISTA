package com.technoio;

import androidx.room.Database;
import androidx.room.RoomDatabase;

import com.technoio.api.OrderDoa;
import com.technoio.api.OrderItemsDoa;
import com.technoio.models.Order;
import com.technoio.models.OrderItems;

@Database(entities = {Order.class, OrderItems.class}, version = 1)
public abstract class AppDatabase extends RoomDatabase {
    public abstract OrderDoa orderDoa();
    public abstract OrderItemsDoa OrderItemsDoa();
}
