package com.tayobus.sugamaga.api.request;

import io.swagger.annotations.ApiModelProperty;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@NoArgsConstructor
@AllArgsConstructor
@Data
public final class UserCustomRequest {

    @ApiModelProperty(value="몸통", example = "1")
    private int body;

    @ApiModelProperty(value="액세서리", example = "2")
    private int ac;

    @ApiModelProperty(value="등", example = "1")
    private int back;

    @ApiModelProperty(value="눈", example = "1")
    private int eye;

    @ApiModelProperty(value="머리", example = "1")
    private int hair;

    @ApiModelProperty(value="모자", example = "1")
    private int hat;

    @ApiModelProperty(value="머리", example = "1")
    private int head;

    @ApiModelProperty(value="입", example = "1")
    private int mouth;

    @ApiModelProperty(value="눈썹", example = "1")
    private int eyebrow;

}