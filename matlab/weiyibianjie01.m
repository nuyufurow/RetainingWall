function [ k ] = weiyibianjie01( n,K )
%此子函数为挡墙底固结时的位移边界条件,并输出划行划列之后的总刚度矩阵
%输入的K为总刚度矩阵，n为挡墙层数
if n==1 
   k=K([4,6],[4,6]);% 采用划行划列，排除1节点三个位移；排除2节点y位移
elseif n==2 
   k=K([4,6,7,9],[4,6,7,9]);% 采用划行划列，排除1节点三个位移；排除2、3节点y位移
elseif n==3 
   k=K([4,6,7,9,10,12],[4,6,7,9,10,12]);% 采用划行划列，排除1节点三个位移；排除2、3、4节点y位移
 elseif n==4 
   k=K([4,6,7,9,10,12,13,15],[4,6,7,9,10,12,13,15]);% 采用划行划列，排除1节点三个位移；排除2、3、4、5节点y位移  

end