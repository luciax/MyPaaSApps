<?php
require_once 'includes/init.php';
require_once 'includes/functions.php';

$services = getenv("VCAP_SERVICES");

if (!$services == null)
{

	$services_json = json_decode($services,true);

	print_r($services_json);
	echo "<br />";
	if ($mysql_config = $services_json['mysql-5.1'][0]["credentials"])
	{

	$DB_NAME= $mysql_config["name"];
    echo "database catalog name: " . $DB_NAME . "<br />";
    
	$DB_USER= $mysql_config["user"];
	echo "database user name: " . $DB_USER . "<br />";
	
	$DB_PASSWORD= $mysql_config["password"];
	echo "database user password: " . $DB_PASSWORD . "<br />";
	
	$DB_HOST= $mysql_config["hostname"];
	echo "database host name: " . $DB_HOST . "<br />";
	
	$DB_PORT= $mysql_config["port"];
	echo "database port number: " . $DB_PORT . "<br />";
	}
	
}

echo 'in dbtest2<br />';
    // clear the results
    $items = '';
    // Get the connection 
echo 'created empty $items <br />';    

    $connection = Database::getConnection();
    
echo 'got connection object <br />';    

// Create the MySQL command by copying the command and
  // splitting into shorter lines and concatenating with periods
  // Drop the final semicolon on the MySQL commmand
  // but don't forget the semicolon for ending the PHP command
    
$query = "CREATE TABLE IF NOT EXISTS `categories` ( "
  . "`cat_id` int(10) unsigned NOT NULL AUTO_INCREMENT, "
  . "`cat_name` varchar(50) NOT NULL, "
  . "`cat_description` text, "
  . "`cat_image` varchar(255) DEFAULT NULL, "
  . "PRIMARY KEY (`cat_id`) "
  . ") ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=6  ";
    
     // Run the query and display appropriate message
  if (!$result = $connection->query($query)) {
    echo "Unable to create table<br />";
  } else {
    echo "Table successfully created<br />";
  }

$query = "INSERT INTO `categories` (`cat_id`, `cat_name`, `cat_description`, `cat_image`) VALUES "
  . "(1, 'Gents', 'Gents'' clothing from the 18th century to modern times!', 'tophat-8-63.jpg'), "
  . "(2, 'Sporting', 'Sporting clothing and gear.', 'wool-6-171.jpg'), "
  . "(3, 'Women', 'Women''s Clothing from the 18th century to modern times', 'gernreich-10-26.jpg') ";
    
     // Run the query and display appropriate message
  if (!$result = $connection->query($query)) {
    echo "Unable to insert into table <br />";
  } else {
    echo "Table successfully populated<br />";
  }
  
  $query = "CREATE TABLE IF NOT EXISTS `contacts` ( "
    . "`id` int(11) unsigned NOT NULL AUTO_INCREMENT, "
    . "`first_name` varchar(50) NOT NULL, "
    . "`last_name` varchar(50) NOT NULL, "
    . "`position` varchar(50) DEFAULT NULL, "
    . "`email` varchar(255) DEFAULT NULL, "
    . "`phone` varchar(20) DEFAULT NULL, "
    . "PRIMARY KEY (`id`) "
    . ") ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=11 ";
    
     // Run the query and display appropriate message
  if (!$result = $connection->query($query)) {
    echo "Unable to create table<br />";
  } else {
    echo "Table successfully created<br />";
  }

$query = "INSERT INTO `contacts` (`id`, `first_name`, `last_name`, `position`, `email`, `phone`) VALUES "
    . "(1, 'Martha', 'Smith', 'none', 'martha@example.com', NULL), "
    . "(2, 'George', 'Smith', 'IT', 'george@example.com', '515-555-1236'), "
    . "(3, 'Jeff', 'Meyers', 'hip hop expert for shure', 'jeff@example.com', NULL), "
    . "(4, 'Peter', 'Meyers', 'none', 'peter@example.com', '515-555-1237'), "
    . "(5, 'Sally', 'Smith', 'none', 'sally@example.com', '515-555-1235'), "
    . "(6, 'Sarah', 'Finder', 'Lost Soul', 'finder@a.com', '555-123-5555'), "
    . "(10, 'Mitchell \"Bud\"', 'O''Reilly', 'none', '', '') ";
    
     // Run the query and display appropriate message
  if (!$result = $connection->query($query)) {
    echo "Unable to insert into table <br />";
  } else {
    echo "Table successfully populated<br />";
  }
  
    $query = "CREATE TABLE IF NOT EXISTS `lots` (  "
      . "`lot_id` int(11) unsigned NOT NULL AUTO_INCREMENT,  "
      . "`lot_name` varchar(50) NOT NULL,  "
      . "`lot_description` text,  "
      . "`lot_image` varchar(255) DEFAULT NULL,  "
      . "`lot_number` int(11) unsigned DEFAULT NULL,  "
      . "`lot_price` decimal(10,2) DEFAULT '0.00',  "
      . "`cat_id` int(11) unsigned DEFAULT NULL,  "
      . "PRIMARY KEY (`lot_id`)  "
      . ") ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=12  ";
    
     // Run the query and display appropriate message
  if (!$result = $connection->query($query)) {
    echo "Unable to create table<br />";
  } else {
    echo "Table successfully created<br />";
  }

$query = "INSERT INTO `lots` (`lot_id`, `lot_name`, `lot_description`, `lot_image`, `lot_number`, `lot_price`, `cat_id`) VALUES "
      . "(2, 'Striped Cotton Tailcoat, America, 1835-1845', 'Orange and white pin-striped twill cotton, double breasted, turn down collar, waist seam, self-fabric buttons, inside single button pockets in each tail, (soiled, faded, cuff edges frayed) good. ', 'gents-striped-8-26.jpg', 26, 20700.00, 2), "
      . "(3, 'Printed & Voided Velvet Evening Gown, 1850s', 'Chocolate brown silk faille with border design of brown and cream roses, uncut and voided velvet printed in shades of brown and cream, full skirt in two tiers, back brass hook & eye closure, glazed linen bodice lining, (seams at waistline weak, minor stains) excellent. ', 'gown-victorian-8-173.jpg', 173, 13800.00, 1), "
      . "(4, 'Dior Couture Wool Cocktail Dress, 1948', 'Unlabeled black melton wool 3 piece ensemble, c/o tulip shape skirt w/ projecting side panel, strapless bodice w/ built-in corset, & face-framing off-the-shoulder shrug, B 36\", W 27\", H 42\", center front bodice L 9.75\", skirt L 31\", excellent. ', 'dior-10-2.jpg', 2, 40250.00, 1), "
      . "(5, 'Pierre Cardin for Mia Farrow Dress, 1967', 'Made exclusively for Mia Farrow in her first starring film role, 1968&#39;s &#34;A Dandy In Aspic&#34;, white wool woven in tiny honey-comb pattern, graduated accordian pleats from collar to hem, circular padded roll collar w/ CF snap, white China silk lining. excellent. ', 'cardin-5-740.jpg', 740, 19550.00, 1), "
      . "(6, 'Black Broadcloth Tailcoat, 1830-1845', 'Fine thin wool broadcloth, double breasted, notched collar, horizontal front and side waist seam, slim long sleeves with notched cuffs, curved tails, black silk satin lining quilted in diamond pattern, padded and quilted chest, black silk covered buttons, (buttons worn) excellent. ', 'gents-black-8-27.jpg', 27, 3450.00, 2), "
      . "(7, 'Naval Officer''s Formal Tailcoat, 1840s', 'Black wool broadcloth, double breast front, missing 3 of 18 raised round gold buttons w/ crossed cannon barrels & \"Ordnance Corps\" text, silver sequin & tinsel embroidered emblem on each square cut tail, quilted black silk lining, very good; ', 'naval-19-173.jpg', 173, 5700.00, 2), "
      . "(8, 'Colorful Striped Wool Bathing Suit, c. 1910', 'Gent''s 1-piece machine knit suit in red, green, black & cream, 3 buttons each shoulder, DLM, Ch 35\", W 32.5\", L 43\", (minor mends, 1 dime size hole in back) good. ', 'swimsuit-striped-9-265.jpg', 265, 1380.00, 3), "
      . "(9, 'Ladies Bathing Costume, Shoes & Floats, c. 1900', 'Marine blue lightweight wool, white sailor collar & trim, button-on skirt, labeled \"Arnold Constable & Co. New York\", B 34\", W 25\", L 40\"; 1 pair black cotton knit thigh-high canvas sole bathing shoes & set of \"Aybad''s Water Wings Patented May 7, 1901\", excellent. ', 'skirted-bathing-1-254.jpg', 254, 510.00, 3), "
      . "(10, 'Frontier Beaded Jacket & Chaps, c. 1920', 'Caramel deerskin leather w/ large glass beads in green & white, Jacket: Chest 42\", W 39\", L 34\", Chap''s inseam 29\", prob. made by Mohawks for Wild West shows or as fraternal costume for Improved Order of Red Men, (leather dry, bead loss) good. ', 'frontier-10-549.jpg', 549, 258.75, 3) ";
    
     // Run the query and display appropriate message
  if (!$result = $connection->query($query)) {
    echo "Unable to insert into table <br />";
  } else {
    echo "Table successfully populated<br />";
  }
  
  // Show the tables
  if ($result = $connection->query("SHOW TABLES")) {
    $count = $result->num_rows;
    echo "Tables: ($count)<br />"; 
    while ($row =$result->fetch_array()) {
    	 echo $row[0]. '<br />';
    }
  }
    
     
