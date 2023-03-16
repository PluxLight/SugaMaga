package com.tayobus.sugamaga.api.request;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
public final class SignRequest {
    private String email;
    private String nickname;
    private String uid;
}