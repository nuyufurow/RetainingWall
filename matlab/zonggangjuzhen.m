function [ K ] = zonggangjuzhen( n,E,A,I,H )
%输出总刚度矩阵
%n——挡墙层数
%H——为每层挡墙层高
 K=zeros((n+1)*3,(n+1)*3);                     %定义初始的全0矩阵,一个节点有三行列
if n==1
 k1=PlaneFrameElementStiffness(E(1),A(1),I(1),H(1,1));  %为单元刚度矩阵（同总刚度矩阵）
 K=k1;                                         %单刚同总刚
elseif n==2
 k1=PlaneFrameElementStiffness(E(1),A(1),I(1),H(1,1));  %一层单元刚度矩阵
 k2=PlaneFrameElementStiffness(E(1),A(2),I(2),H(1,2));  %二层单元刚度矩阵
 K=PlaneFrameAssemble(K,k1,1,2);               %将k1组装进K
 K=PlaneFrameAssemble(K,k2,2,3);               %将k2组装进K
elseif n==3
 k1=PlaneFrameElementStiffness(E(1),A(1),I(1),H(1,1));  %一层单元刚度矩阵
 k2=PlaneFrameElementStiffness(E(1),A(2),I(2),H(1,2));  %二层单元刚度矩阵
 k3=PlaneFrameElementStiffness(E(1),A(3),I(3),H(1,3));  %三层单元刚度矩阵
 K=PlaneFrameAssemble(K,k1,1,2);               %将k1组装进K
 K=PlaneFrameAssemble(K,k2,2,3);               %将k2组装进K
 K=PlaneFrameAssemble(K,k3,3,4);               %将k3组装进K
elseif n==4
 k1=PlaneFrameElementStiffness(E(1),A(1),I(1),H(1,1));  %一层单元刚度矩阵
 k2=PlaneFrameElementStiffness(E(1),A(2),I(2),H(1,2));  %二层单元刚度矩阵
 k3=PlaneFrameElementStiffness(E(1),A(3),I(3),H(1,3));  %三层单元刚度矩阵
 k4=PlaneFrameElementStiffness(E(1),A(4),I(4),H(1,4));  %四层单元刚度矩阵
 K=PlaneFrameAssemble(K,k1,1,2);               %将k1组装进K
 K=PlaneFrameAssemble(K,k2,2,3);               %将k2组装进K
 K=PlaneFrameAssemble(K,k3,3,4);               %将k3组装进K
 K=PlaneFrameAssemble(K,k4,4,5);               %将k4组装进K
end
end

