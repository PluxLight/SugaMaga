package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.db.entity.History;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface HistoryRepository extends JpaRepository<History, Integer> {
    List<History> findAllByUid(String uid);
}
