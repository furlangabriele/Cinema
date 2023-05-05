function makeCode(div_id, item_id) {
	document.getElementById(div_id).innerHTML = "";
	var qrcode = new QRCode(div_id);
	var elText = item_id;
	qrcode.makeCode(elText);
}