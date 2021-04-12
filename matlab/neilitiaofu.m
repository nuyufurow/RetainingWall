function [ M0 ] = neilitiaofu(M01,M02,T)
%本子函数用于内力调幅内力组合
%M01为neilijisuan01( K,k,f,n,Q,H )求得的墙底固结时，各节点内力
%M02为neilijisuan02( K,k,f,n,Q,H )求得的墙底铰接时，各节点内力
%M0为调幅后用于配筋的各节点内力，经推导，弯矩和剪力都可以用M01*T+M02*(1-T)调幅
%T为内力调幅系数
    M0=M01*T+M02*(1-T);

end
