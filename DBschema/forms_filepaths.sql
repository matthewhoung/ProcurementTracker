CREATE TABLE vplus_dev.forms_filepaths (
    file_id INT AUTO_INCREMENT PRIMARY KEY,
    form_id INT NOT NULL,
    uploader_id INT NOT NULL,
    file_name VARCHAR(255) NOT NULL,
    file_path VARCHAR(1024) NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (form_id) REFERENCES forms(id)
);
