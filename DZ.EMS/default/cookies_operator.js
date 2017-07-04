/*新增cookie*/
function addCookie(name,value,expiresHours){ 	
	var cookieString=name+"="+escape(value); 
	//判断是否设置过期时间 
	if(expiresHours>0){ 
		var date=new Date(); 
		date.setTime(date.getTime()+expiresHours*3600*1000*24); 
		cookieString=cookieString+"; expires="+date.toGMTString()+";domain=.ihuge.com;path=/"; 		
	} 
	document.cookie=cookieString; 
}
/*获取cookie*/
function getCookie(name){
	var strCookie=document.cookie; 
	var arrCookie=strCookie.split(";"); 
	for(var i=0;i<arrCookie.length;i++){ 
		var arr=arrCookie[i].split("="); 
		if(trim(arr[0])==name){
			return unescape(arr[1]); 
		}
	}
	return "";
}
/*删除cookie*/
function deleteCookie(name){ 
	var date=new Date(); 
	date.setTime(date.getTime()-10000); 
	document.cookie=name+"=v; expires="+date.toGMTString()+";domain=.ihuge.com;path=./"; 
}
//去左右空格;
function trim(s){
    return s.replace(/(^\s*)|(\s*$)/g, "");
}