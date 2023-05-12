create table search_log
(
    log_id       int auto_increment
        primary key,
    search_query varchar(40) not null,
    search_time  datetime    null
);

INSERT INTO woojin_search_image.search_log (log_id, search_query, search_time) VALUES (8, 'test', '2023-05-12 15:08:15');
INSERT INTO woojin_search_image.search_log (log_id, search_query, search_time) VALUES (9, '안녕', '2023-05-12 15:08:23');
INSERT INTO woojin_search_image.search_log (log_id, search_query, search_time) VALUES (10, '안녕하세요', '2023-05-12 14:38:31');