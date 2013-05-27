<?php
/**
 * Database class
 * For one point of database access
 */
class Database
{
	
// rest of the code will go here
/**
   * User name to connect to database
   * @var string $_mysqlUser
   */
  private static $_mysqlUser = 'php24sql';
  /**
   * Password to connect to database
   * @var string $_mysqlPass
   */
  private static $_mysqlPass = 'hJQV8RTe5t';
  /**
   * Database name
   * @var string $_mysqlDb
   */
  private static $_mysqlDb = 'smithside';
  /**
   * Hostname for the server
   * @var string $_hostName
   */
  private static $_hostName = '127.0.0.1';
  
  /**
   * Database connection
   * @var Mysqli $connection
   */
  private static $_connection = NULL;
  
/**
   * Get the Database Connection
   * 
   * @return Mysqli
   */
public static function getConnection() {

$services = getenv("VCAP_SERVICES");

if (!$services == null)
{
	
$services_json = json_decode($services,true);

$mysql_config = $services_json["mysql-5.1"][0]["credentials"];

$DB_NAME= $mysql_config["name"];

$DB_USER= $mysql_config["user"];

$DB_PASSWORD= $mysql_config["password"];

$DB_HOST= $mysql_config["hostname"];

$DB_PORT= $mysql_config["port"];



  if (!self::$_connection) {
    self::$_connection = @new mysqli($DB_HOST, $DB_USER,
      $DB_PASSWORD, $DB_NAME);

    if (self::$_connection->connect_error) {

      die('Connect Error: ' . self::$_connection->connect_error);
    } 
  }

} else {	
  if (!self::$_connection) {
    self::$_connection = @new mysqli(self::$_hostName, self::$_mysqlUser,
      self::$_mysqlPass, self::$_mysqlDb);
    if (self::$_connection->connect_error) {
      die('Connect Error: ' . self::$_connection->connect_error);
    } 
  }
 
}
  return self::$_connection;
}



 public static function prep($value) { 
    if (MAGIC_QUOTES_ACTIVE) {
      // If magic quotes is active, remove the slashes
      $value = stripslashes($value);
    }
    // Escape special characters to avoid SQL injections
    $value = self::$_connection->real_escape_string($value);
    return $value;
  }

/**
 * Constructor
 */
private function __construct(){
}

}