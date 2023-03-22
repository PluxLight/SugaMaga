package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.api.request.CustomRequest;
import com.tayobus.sugamaga.db.entity.UserCustom;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface CustomRepository extends JpaRepository<UserCustom, Long> {
    Optional<UserCustom> save(CustomRequest customRequest);
    Optional<UserCustom> findByUserIdx(long userIdx);
}