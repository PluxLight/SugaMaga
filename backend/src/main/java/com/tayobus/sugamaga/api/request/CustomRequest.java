package com.tayobus.sugamaga.api.request;

import lombok.*;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
@ToString
public final class CustomRequest {
    private int userCustomIdx;
    private int cap;
    private int hair;
    private int face;
    private int eyes;
    private int mouse;
    private int body;
    private String uid;
    private long userIdx;

}