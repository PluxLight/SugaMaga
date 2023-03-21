package com.tayobus.sugamaga.db.entity;

import io.swagger.annotations.ApiModelProperty;
import lombok.*;
import org.hibernate.annotations.DynamicInsert;

import javax.persistence.*;

@ToString
@Data
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Builder
@Table(name = "user_custom")
@DynamicInsert
public class UserCustom {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @JoinColumn(name = "user_custom_idx")
    private int userCustomIdx;

    @JoinColumn(name = "user_idx")
    private Long userIdx;

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
