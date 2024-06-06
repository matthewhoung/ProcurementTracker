CREATE TABLE vplus_dev.payable_signatures (
    sign_id INT AUTO_INCREMENT PRIMARY KEY,
    form_id INT,
    user_id INT,
    role_id INT,
    is_checked TINYINT(1),
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (form_id) REFERENCES forms(id),
    FOREIGN KEY (role_id) REFERENCES roles(role_id)
);
