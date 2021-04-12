function [ MM ] = neilizuhe(n,Mg,Mmaxg)
%本子函数用于将neilijisuan子函数和kuazhongzuidaM子函数组合成1个矩阵
% Mg为neilitiaofu(M01,M02,T)得到的节点弯矩和剪力
% Mmaxg为kuazhongzuidaM得到的跨中最大弯矩

if n==1
    MMg=[Mg(1,1) Mmaxg(1) Mg(1,2)];
    MM=MMg;
elseif n==2
    MMg=[Mg(1,1) Mmaxg(1) Mg(1,2) Mmaxg(2) Mg(1,4)];
    MM=MMg;   
elseif n==3
    MMg=[Mg(1,1) Mmaxg(1) Mg(1,2) Mmaxg(2) Mg(1,4) Mmaxg(3) Mg(1,6)];
    MM=MMg;
elseif n==4
    MMg=[Mg(1,1) Mmaxg(1) Mg(1,2) Mmaxg(2) Mg(1,4) Mmaxg(3) Mg(1,6) Mmaxg(4) Mg(1,8)];
    MM=MMg;
end

end

