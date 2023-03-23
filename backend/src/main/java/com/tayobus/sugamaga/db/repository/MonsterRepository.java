package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.db.entity.Monster;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface MonsterRepository extends JpaRepository<Monster, Integer> {
    List<Monster> findByMonsterIdx(int equipItemIdx);
}
