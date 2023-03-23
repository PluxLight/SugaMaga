package com.tayobus.sugamaga.api.request;

import io.swagger.annotations.ApiModelProperty;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class HistoryRequest {

    @ApiModelProperty(value="게임 방 번호", example = "1")
    private int gameRoomIdx;

    @ApiModelProperty(value="유저 uid", example = "userUid")
    private String uid;

    @ApiModelProperty(value="게임 결과 등수", example = "2")
    private int resultRank;

    @ApiModelProperty(value="게임 결과 처치 수", example = "3")
    private int resultKill;

    @ApiModelProperty(value="게임 맵 번호", example = "1")
    private int mapIdx;
}
