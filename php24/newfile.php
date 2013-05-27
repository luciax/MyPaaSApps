<?php
echo "in newfile.php <br />";

$service_type = getenv("service_type");
echo 'service_type = ' . $service_type . '<br />';

$services = getenv("VCAP_SERVICES");

echo "<br /> <br /> VCAP_SERVICES: <br /> <br />";

if (!$services == null)
{

	$services_json = json_decode($services,true);

	print_r($services_json);
	echo "<br /><br />";
	if ($mysql_config = $services_json[$service_type][0]["credentials"])
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

} else {
		
		echo "could not get VCAP_SERVICES object <br />";
	}

