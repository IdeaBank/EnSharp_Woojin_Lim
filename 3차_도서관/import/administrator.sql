create table administrator
(
    id       varchar(45) not null
        primary key,
    password varchar(45) not null
);

INSERT INTO woojin_library.administrator (id, password) VALUES ('admin123', 'admin123');
