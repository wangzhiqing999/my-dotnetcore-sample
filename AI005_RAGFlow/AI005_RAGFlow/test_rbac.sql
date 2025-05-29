-- test_rbac.permissions definition

CREATE TABLE `permissions` (
  `permission_id` int(11) NOT NULL AUTO_INCREMENT COMMENT '权限唯一标识',
  `permission_name` varchar(50) NOT NULL COMMENT '权限名称，如创建文章、删除用户等',
  `description` text COMMENT '权限描述，说明该权限的具体作用',
  PRIMARY KEY (`permission_id`),
  UNIQUE KEY `permission_name` (`permission_name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COMMENT='权限信息表';


-- test_rbac.roles definition

CREATE TABLE `roles` (
  `role_id` int(11) NOT NULL AUTO_INCREMENT COMMENT '角色唯一标识',
  `role_name` varchar(50) NOT NULL COMMENT '角色名称，如管理员、普通用户等',
  `description` text COMMENT '角色描述，说明该角色的职责和权限范围',
  PRIMARY KEY (`role_id`),
  UNIQUE KEY `role_name` (`role_name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COMMENT='角色信息表';


-- test_rbac.users definition

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT COMMENT '用户唯一标识',
  `username` varchar(50) NOT NULL COMMENT '用户名，用于登录和识别用户',
  `password` varchar(255) NOT NULL COMMENT '用户密码，加密存储',
  `email` varchar(100) DEFAULT NULL COMMENT '用户邮箱，可用于找回密码等操作',
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COMMENT='用户信息表';


-- test_rbac.role_permissions definition

CREATE TABLE `role_permissions` (
  `role_id` int(11) NOT NULL COMMENT '关联的角色 ID',
  `permission_id` int(11) NOT NULL COMMENT '关联的权限 ID',
  PRIMARY KEY (`role_id`,`permission_id`),
  KEY `permission_id` (`permission_id`),
  CONSTRAINT `role_permissions_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `roles` (`role_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `role_permissions_ibfk_2` FOREIGN KEY (`permission_id`) REFERENCES `permissions` (`permission_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='角色与权限关联表';


-- test_rbac.user_roles definition

CREATE TABLE `user_roles` (
  `user_id` int(11) NOT NULL COMMENT '关联的用户 ID',
  `role_id` int(11) NOT NULL COMMENT '关联的角色 ID',
  PRIMARY KEY (`user_id`,`role_id`),
  KEY `role_id` (`role_id`),
  CONSTRAINT `user_roles_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `user_roles_ibfk_2` FOREIGN KEY (`role_id`) REFERENCES `roles` (`role_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='用户与角色关联表';
