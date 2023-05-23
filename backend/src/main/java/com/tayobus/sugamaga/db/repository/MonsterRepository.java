package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.db.entity.Monster;
import org.springframework.data.jpa.repository.JpaRepository;

public interface MonsterRepository extends JpaRepository<Monster, Integer> {
    Monster findByMonsterIdx(int equipItemIdx);
}
