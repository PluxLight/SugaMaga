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
@Table(name = "equipment_item")
public class EquipmentItem {
    @Id
    @JoinColumn(name = "equip_item_idx")
    private int equipItemIdx;

    @Column(nullable = false)
    @JoinColumn(name = "equip_name")
    private String equipName;

    @Column(nullable = false)
    @JoinColumn(name = "equip_value")
    private int equipValue;

    @Column(nullable = false)
    @JoinColumn(name = "equip_speed")
    private float equipSpeed;

    @Column(nullable = false)
    @JoinColumn(name = "skill_name")
    private String skillName;

    @Column(nullable = false)
    @JoinColumn(name = "skill_damage")
    private int skillDamage;

    @Column(nullable = false)
    @JoinColumn(name = "skill_cooltime")
    private float skillCooltime;

    @Column(nullable = false)
    @JoinColumn(name = "equip_type")
    private int equipType;

}
