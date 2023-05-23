package com.tayobus.sugamaga.db.entity;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Builder
@Table(name = "drop_table")
public class DropTable {
    @Id
    @JoinColumn(name = "drop_table_idx")
    private int dropTableIdx;

    @JoinColumn(name = "table_idx")
    private int tableIdx;

    @JoinColumn(name = "item_code")
    private int itemCode;

    @JoinColumn(name = "item_name")
    private String itemName;

    private int percentage;
}
