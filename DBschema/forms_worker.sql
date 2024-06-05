create table vplus_dev.forms_worker(
	worker_id INT AUTO_INCREMENT PRIMARY KEY,
    form_id int,
    worker_type_id int,
    worker_team_id int,
    FOREIGN KEY (form_id) REFERENCES forms(id),
    foreign key (worker_type_id) references worker_types(worker_type_id),
    foreign key (worker_team_id) references worker_teams(worker_team_id)
);