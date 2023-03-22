package com.tayobus.sugamaga.api.request;

import io.swagger.annotations.ApiModelProperty;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@NoArgsConstructor
@AllArgsConstructor
@Data
public final class UserCustomRequest {

    @ApiModelProperty(value="모자", example = "1")
    private int cap;

    @ApiModelProperty(value="머리", example = "1")
    private int hair;

    @ApiModelProperty(value="얼굴", example = "1")
    private int face;

    @ApiModelProperty(value="눈", example = "1")
    private int eyes;

    @ApiModelProperty(value="입", example = "1")
    private int mouse;

    @ApiModelProperty(value="몸통", example = "1")
    private int body;
}