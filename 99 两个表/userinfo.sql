/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : studymysql

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2022-05-04 18:35:21
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for userinfo
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of userinfo
-- ----------------------------
INSERT INTO `userinfo` VALUES ('1', '关羽', '30');
INSERT INTO `userinfo` VALUES ('2', '张飞', '30');
INSERT INTO `userinfo` VALUES ('3', '高顺', '21');
INSERT INTO `userinfo` VALUES ('4', '姜维', '10');
INSERT INTO `userinfo` VALUES ('6', '马超', '40');
INSERT INTO `userinfo` VALUES ('7', '张辽', '40');
