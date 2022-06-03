package com.technoio.models;

import androidx.room.Embedded;
import androidx.room.Relation;

import java.util.List;

public class OrderAndOrderItem {
    @Embedded public Order order;

    @Relation(
        parentColumn = "id",
        entityColumn = "order_id"
    )
    public OrderItems orderItems;
}
