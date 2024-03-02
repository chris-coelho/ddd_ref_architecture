
    drop table if exists accounts cascade

    drop table if exists movies cascade

    drop table if exists reviews cascade

    create table accounts (
        Id uuid not null,
       modified_at timestamp not null,
       name varchar(255) not null,
       email varchar(255) not null,
       created_on timestamp not null,
       primary key (Id),
      unique (email)
    )

    create table movies (
        Id uuid not null,
       modified_at timestamp not null,
       name varchar(255) not null,
       year int4 not null,
       genre varchar(255) not null,
       rate float8,
       count int8,
       primary key (Id)
    )

    create table reviews (
        Id uuid not null,
       modified_at timestamp not null,
       rate float8 not null,
       comments text,
       account_id uuid not null,
       occurred_on timestamp not null,
       movie_id uuid,
       primary key (Id)
    )

    alter table reviews 
        add constraint fk_movie_id 
        foreign key (movie_id) 
        references movies
