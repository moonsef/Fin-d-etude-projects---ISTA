package com.technoio.api;

import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.Query;
import androidx.room.RawQuery;
import androidx.room.Transaction;

import com.technoio.models.Order;
import com.technoio.models.OrderAndOrderItem;
import com.technoio.models.OrderItems;

import java.util.List;


@Dao
public interface OrderDoa {

    @Insert
    long insertOrder(Order order);

    @Query("DELETE FROM orders where id=:id")
    void deleteById(long id);

    @Transaction
    @Query("SELECT * FROM orders where client_uuid=:uuid")
    List<OrderAndOrderItem> getOrdersAndOrderItems(String uuid);


    @Query("select sum((ot.list_prix - (ot.list_prix * ot.discount / 100)) * ot.quantity) from orders o inner join order_items ot on ot.order_id=o.id where o.client_uuid=:uuid")
    int getTotalPrixCommand(String uuid);


    @Query("delete from orders")
    void deleteAll();

}

