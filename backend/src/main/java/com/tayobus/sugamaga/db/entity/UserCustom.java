package com.tayobus.sugamaga.db.entity;

import io.swagger.annotations.ApiModelProperty;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.annotations.DynamicInsert;

import javax.persistence.*;

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

    @Column(nullable = false)
    @ApiModelProperty(value="유저 uid", example = "a1b2c3d4")
    private String uid;

    @Column(nullable = false)
    @ApiModelProperty(value="모자", example = "1")
    private int cap;

    @Column(nullable = false)
    @ApiModelProperty(value="머리", example = "1")
    private int hair;

    @Column(nullable = false)
    @ApiModelProperty(value="얼굴", example = "1")
    private int face;

    @Column(nullable = false)
    @ApiModelProperty(value="눈", example = "1")
    private int eyes;

    @Column(nullable = false)
    @ApiModelProperty(value="입", example = "1")
    private int mouse;

    @Column(nullable = false)
    @ApiModelProperty(value="몸통", example = "1")
    private int body;

/*    @OneToOne
    @JoinColumn(name = "uid")
    private User user;*/

}
