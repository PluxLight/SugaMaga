package com.tayobus.sugamaga.api.request;

import io.swagger.annotations.ApiModelProperty;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@NoArgsConstructor
@AllArgsConstructor
@Data
public final class NicknameRequest {

    @ApiModelProperty(value="닉네임", example = "GoodNickname")
    private String nickname;

}