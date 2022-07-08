/*
Navicat MySQL Data Transfer

Source Server         : 127.0.0.1
Source Server Version : 50620
Source Host           : localhost:3306
Source Database       : sexybaseball

Target Server Type    : MYSQL
Target Server Version : 50620
File Encoding         : 65001

Date: 2021-03-28 14:43:30
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for x_role
-- ----------------------------
DROP TABLE IF EXISTS `x_role`;
CREATE TABLE `x_role` (
  `iUserId` int(11) NOT NULL AUTO_INCREMENT,
  `acc_name` varchar(64) NOT NULL DEFAULT '',
  `acc_password` varchar(64) NOT NULL DEFAULT '',
  `acc_local_id` varchar(64) DEFAULT '',
  `rol_name` varchar(64) NOT NULL DEFAULT '',
  `iLevel` int(4) unsigned NOT NULL DEFAULT '0',
  `iExp` int(4) NOT NULL DEFAULT '0',
  `iLoginTime` int(11) DEFAULT '0',
  `In_Time` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`iUserId`),
  KEY `acc_name` (`acc_name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of x_role
-- ----------------------------
INSERT INTO `x_role` VALUES ('1', '666666', '666666', '', '666666', '2', '30', '2', '2021-03-28 14:35:59');
INSERT INTO `x_role` VALUES ('2', '111111', '111111', '', '111111', '1', '0', '1', '2021-03-28 14:35:59');
INSERT INTO `x_role` VALUES ('3', '888888', '888888', '', '888888', '7', '175', '3', '2021-03-28 14:35:59');
INSERT INTO `x_role` VALUES ('4', '222222', '222222', '', '222222', '0', '0', '0', '2021-03-28 14:37:34');
INSERT INTO `x_role` VALUES ('5', '222', '222', '', '222', '0', '0', '0', '2021-03-28 14:42:18');
