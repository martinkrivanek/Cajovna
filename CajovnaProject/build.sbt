name := """CajovnaProject"""

version := "1.0-SNAPSHOT"

lazy val root = (project in file(".")).enablePlugins(PlayJava)

scalaVersion := "2.11.1"

libraryDependencies ++= Seq(
  javaJdbc,
  javaEbean,
  cache,
  javaWs,
  "org.hibernate" % "hibernate-entitymanager" % "4.3.6.Final",
  "mysql" % "mysql-connector-java" % "5.1.33"
)
