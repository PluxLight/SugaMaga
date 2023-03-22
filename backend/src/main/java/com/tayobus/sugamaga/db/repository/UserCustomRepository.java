package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.db.entity.UserCustom;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface UserCustomRepository extends JpaRepository<UserCustom, Integer> {
    Optional<UserCustom> findByUserCustomIdx(int idx);
    UserCustom findByUid(String uid);
}
