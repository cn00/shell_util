#!/bin/bash  
  
#andyzhang  
#2013-08-01  
  
MIN_LAG=400 #最小时延，单位毫秒  
MAX_LAG=800 #最大时延，单位毫秒  
  
IN_FLAG=true #对上行的限制，true为开启限制，false为关闭限制  
OUT_FLAG=true #对下行的限制，true为开启限制，false为关闭限制  
  
in_speed=20 #上行速度，kb为单位  
out_speed=20 #下行速度，kb为单位  
  
lose_rate_up=0.5 #上行丢包率  
lose_rate_down=0.5 #下行丢包率  
  
############################################  
#上面部分是配置部分，以下代码为命令执行过程#  
############################################  
  
((LAG_RANGE=MAX_LAG-MIN_LAG ))#  
  
INTERVAL=0.1 # time in between changes in latency (in seconds)  
  
if(${IN_FLAG});then  
    #ipfw add 100 pipe 1 ip from 192.168.0.1/24 to any in  
    ipfw add 100 pipe 1 ip from any to 192.168.2.1/24 in  
fi  
  
if(${OUT_FLAG});then  
    #ipfw add 200 pipe 2 ip from any to 192.168.0.1/24 out  
    ipfw add 200 pipe 2 ip from 192.168.2.1/24 to any out  
fi  
  
# set up initial random delay  
(( delay = RANDOM % LAG_RANGE + MIN_LAG ))  
  
# start ping background process and stash result in a file  
ping -i 0.05 localhost > ping_out &  
PING_PID=$!   
  
while(true)  
do  
    sleep $INTERVAL  
  
    if(${IN_FLAG});then  
        # change the delay to a new random value  
        ((delay=RANDOM % LAG_RANGE+MIN_LAG))  
        ipfw pipe 1 config delay ${delay}ms bw ${in_speed}Kb plr ${lose_rate_up}  
        echo "setting update delay to" $delay "ms" $in_speed"kb"  
    fi  
  
    if(${OUT_FLAG});then  
        # change the delay to a new random value  
        ((delay=RANDOM%LAG_RANGE+MIN_LAG))  
        ipfw pipe 2 config delay ${delay}ms bw ${out_speed}Kb plr ${lose_rate_down}  
        echo "setting download delay to" $delay "ms"$out_speed"kb"  
    fi  
  
done  
  
# kill the ping process  
kill $PING_PID  
  
# remove the pipe and the associated rule  
if(${IN_FLAG});then  
    ipfw delete 100  
    ipfw pipe 1 delet  
fi  
  
if(${OUT_FLAG});then  
    ipfw delete 200  
    ipfw pipe 2 delete  
fi