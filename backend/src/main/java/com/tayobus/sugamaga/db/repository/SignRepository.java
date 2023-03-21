package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.db.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface SignRepository extends JpaRepository<User, Long> {
    Optional<User> findByEmail(String email);
}