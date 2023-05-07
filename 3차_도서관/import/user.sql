create table user
(
    id           varchar(45) not null
        primary key,
    password     varchar(45) not null,
    name         varchar(45) not null,
    birth_year   int         not null,
    phone_number varchar(45) not null,
    address      varchar(45) not null,
    constraint id_UNIQUE
        unique (id)
);

INSERT INTO woojin_library.user (id, password, name, birth_year, phone_number, address) VALUES ('userid09', 'userpw11', '임우진', 2001, '010-8302-3090', '서울특별시 종로구 청와대로 1');
INSERT INTO woojin_library.user (id, password, name, birth_year, phone_number, address) VALUES ('userid12', 'userpw12', 'Woojin', 2000, '010-8302-3090', '경기도 고양시 일산동구 위시티 1로 7');
