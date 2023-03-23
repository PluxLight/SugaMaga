package com.tayobus.sugamaga.db.entity;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.Table;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Builder
@Table(name = "consumable_item")
public class ConsumableItem {
    @Id
    @JoinColumn(name = "consumable_item_idx")
    private int consumableItemIdx;

    @JoinColumn(name = "consumable_name")
    private String consumableName;

    @JoinColumn(name = "consumable_category")
    private String consumableCategory;

    @JoinColumn(name = "consumable_value")
    private int consumableValue;
}
