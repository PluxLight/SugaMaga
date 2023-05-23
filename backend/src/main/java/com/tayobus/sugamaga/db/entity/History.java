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
@Table(name = "user_game_result")
public class History {
    @Id
    @JoinColumn(name = "user_game_result_idx")
    private int userGameResultIdx;

    @JoinColumn(name = "game_room_idx")
    private int gameRoomIdx;

    private String uid;

    @JoinColumn(name = "result_rank")
    private int resultRank;

    @JoinColumn(name = "result_kill")
    private int resultKill;

    @JoinColumn(name = "map_idx")
    private int mapIdx;
}
