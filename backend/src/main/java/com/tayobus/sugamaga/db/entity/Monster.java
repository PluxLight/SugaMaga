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
@Table(name = "monster")
public class Monster {
    @Id
    @JoinColumn(name = "monster_idx")
    private int monsterIdx;

    @Column(nullable = false)
    @JoinColumn(name = "drop_table")
    private int dropTable;

    @Column(nullable = false)
    @JoinColumn(name = "monster_name")
    private String monsterName;

}
