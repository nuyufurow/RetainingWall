function [ Ass ] =zhengjinshuchu( Asmax )
%本子函数输出正筋
%   此处显示详细说明
AA00=[12 200 565;
      12 190 595;
      12 180 628;
      12 170 665;
      12 160 707;
      12 150 754;
      14 200 770;
      12 140 808;
      14 190 810;
      14 180 855;
      12 130 870;
      14 170 906;
      12 120 942;
      14 160 962;
      16 200 1005;
      14 150 1026;
      12 110 1028;
      16 190 1058;
      14 140 1100;
      16 180 1117;
      12 100 1131;
      16 170 1183;
      14 130 1184;
      16 160 1257;
      18 200 1272;
      14 120 1283;
      18 190 1339;
      16 150 1340;
      14 110 1399;
      18 180 1414;
      16 140 1436;
      18 170 1497;
      14 100 1539;
      16 130 1547;
      20 200 1571;
      18 160 1590;
      20 190 1653;
      16 120 1676;
      18 150 1696;
      20 180 1745;
      18 140 1818;
      16 110 1828;
      20 170 1848;
      22 200 1901;
      18 130 1957;
      20 160 1963;
      22 190 2001;
      16 100 2011;
      20 150 2094;
      22 180 2112;
      18 120 2121;
      22 170 2236;
      20 140 2244;
      18 110 2313;
      22 160 2376;
      20 130 2417;
      25 200 2454;
      22 150 2534;
      18 100 2545;
      25 190 2584;
      20 120 2618;
      22 140 2715;
      25 180 2727;
      20 110 2856;
      25 170 2887;
      22 130 2924;
      25 160 3068;
      20 100 3142;
      22 120 3168;
      25 150 3272;
      22 110 3456;
      25 140 3506;
      25 130 3776;
      22 100 3801;
      25 120 4091;
      25 110 4462;
      25 100 4909];

  if Asmax<=AA00(1,3)
      Ass=[AA00(1,1) AA00(1,2) AA00(1,3)];
  elseif Asmax>AA00(1,3) && Asmax<=AA00(2,3)
      Ass=[AA00(2,1) AA00(2,2) AA00(2,3)];
  elseif Asmax>AA00(2,3) && Asmax<=AA00(3,3)
      Ass=[AA00(3,1) AA00(3,2) AA00(3,3)];
  elseif Asmax>AA00(3,3) && Asmax<=AA00(4,3)
      Ass=[AA00(4,1) AA00(4,2) AA00(4,3)];  
  elseif Asmax>AA00(4,3) && Asmax<=AA00(5,3)
      Ass=[AA00(5,1) AA00(5,2) AA00(5,3)];  
  elseif Asmax>AA00(5,3) && Asmax<=AA00(6,3)
      Ass=[AA00(6,1) AA00(6,2) AA00(6,3)];       
   elseif Asmax>AA00(6,3) && Asmax<=AA00(7,3)
      Ass=[AA00(7,1) AA00(7,2) AA00(7,3)];     
   elseif Asmax>AA00(7,3) && Asmax<=AA00(8,3)
      Ass=[AA00(8,1) AA00(8,2) AA00(8,3)];     
   elseif Asmax>AA00(8,3) && Asmax<=AA00(9,3)
      Ass=[AA00(9,1) AA00(9,2) AA00(9,3)];       
   elseif Asmax>AA00(9,3) && Asmax<=AA00(10,3)
      Ass=[AA00(10,1) AA00(10,2) AA00(10,3)];        
    elseif Asmax>AA00(10,3) && Asmax<=AA00(11,3)
      Ass=[AA00(11,1) AA00(11,2) AA00(11,3)]; 
    elseif Asmax>AA00(11,3) && Asmax<=AA00(12,3)
      Ass=[AA00(12,1) AA00(12,2) AA00(12,3)];      
    elseif Asmax>AA00(12,3) && Asmax<=AA00(13,3)
      Ass=[AA00(13,1) AA00(13,2) AA00(13,3)];       
    elseif Asmax>AA00(13,3) && Asmax<=AA00(14,3)
      Ass=[AA00(14,1) AA00(14,2) AA00(14,3)];  
    elseif Asmax>AA00(14,3) && Asmax<=AA00(15,3)
      Ass=[AA00(15,1) AA00(15,2) AA00(15,3)];        
    elseif Asmax>AA00(15,3) && Asmax<=AA00(16,3)
      Ass=[AA00(16,1) AA00(16,2) AA00(16,3)];       
    elseif Asmax>AA00(16,3) && Asmax<=AA00(17,3)
      Ass=[AA00(17,1) AA00(17,2) AA00(17,3)];  
    elseif Asmax>AA00(17,3) && Asmax<=AA00(18,3)
      Ass=[AA00(18,1) AA00(18,2) AA00(18,3)];       
    elseif Asmax>AA00(18,3) && Asmax<=AA00(19,3)
      Ass=[AA00(19,1) AA00(19,2) AA00(19,3)]; 
    elseif Asmax>AA00(19,3) && Asmax<=AA00(20,3)
      Ass=[AA00(20,1) AA00(20,2) AA00(20,3)]; 
    elseif Asmax>AA00(20,3) && Asmax<=AA00(21,3)
      Ass=[AA00(21,1) AA00(21,2) AA00(21,3)];       
    elseif Asmax>AA00(21,3) && Asmax<=AA00(22,3)
      Ass=[AA00(22,1) AA00(22,2) AA00(22,3)];      
    elseif Asmax>AA00(22,3) && Asmax<=AA00(23,3)
      Ass=[AA00(23,1) AA00(23,2) AA00(23,3)];        
    elseif Asmax>AA00(23,3) && Asmax<=AA00(24,3)
      Ass=[AA00(24,1) AA00(24,2) AA00(24,3)];       
    elseif Asmax>AA00(24,3) && Asmax<=AA00(25,3)
      Ass=[AA00(25,1) AA00(25,2) AA00(25,3)];        
    elseif Asmax>AA00(25,3) && Asmax<=AA00(26,3)
      Ass=[AA00(26,1) AA00(26,2) AA00(26,3)];       
    elseif Asmax>AA00(26,3) && Asmax<=AA00(27,3)
      Ass=[AA00(27,1) AA00(27,2) AA00(27,3)];   
    elseif Asmax>AA00(27,3) && Asmax<=AA00(28,3)
      Ass=[AA00(28,1) AA00(28,2) AA00(28,3)];      
    elseif Asmax>AA00(28,3) && Asmax<=AA00(29,3)
      Ass=[AA00(29,1) AA00(29,2) AA00(29,3)];       
    elseif Asmax>AA00(29,3) && Asmax<=AA00(30,3)
      Ass=[AA00(30,1) AA00(30,2) AA00(30,3)];        
    elseif Asmax>AA00(30,3) && Asmax<=AA00(31,3)
      Ass=[AA00(31,1) AA00(31,2) AA00(31,3)];     
    elseif Asmax>AA00(31,3) && Asmax<=AA00(32,3)
      Ass=[AA00(32,1) AA00(32,2) AA00(32,3)];       
    elseif Asmax>AA00(32,3) && Asmax<=AA00(33,3)
      Ass=[AA00(33,1) AA00(33,2) AA00(33,3)];       
    elseif Asmax>AA00(33,3) && Asmax<=AA00(34,3)
      Ass=[AA00(34,1) AA00(34,2) AA00(34,3)]; 
    elseif Asmax>AA00(34,3) && Asmax<=AA00(35,3)
      Ass=[AA00(35,1) AA00(35,2) AA00(35,3)];      
    elseif Asmax>AA00(35,3) && Asmax<=AA00(36,3)
      Ass=[AA00(36,1) AA00(36,2) AA00(36,3)];       
    elseif Asmax>AA00(36,3) && Asmax<=AA00(37,3)
      Ass=[AA00(37,1) AA00(37,2) AA00(37,3)];      
    elseif Asmax>AA00(37,3) && Asmax<=AA00(38,3)
      Ass=[AA00(38,1) AA00(38,2) AA00(38,3)];      
    elseif Asmax>AA00(38,3) && Asmax<=AA00(39,3)
      Ass=[AA00(39,1) AA00(39,2) AA00(39,3)];      
    elseif Asmax>AA00(39,3) && Asmax<=AA00(40,3)
      Ass=[AA00(40,1) AA00(40,2) AA00(40,3)];       
    elseif Asmax>AA00(40,3) && Asmax<=AA00(41,3)
      Ass=[AA00(41,1) AA00(41,2) AA00(41,3)];      
    elseif Asmax>AA00(41,3) && Asmax<=AA00(42,3)
      Ass=[AA00(42,1) AA00(42,2) AA00(42,3)];       
    elseif Asmax>AA00(42,3) && Asmax<=AA00(43,3)
      Ass=[AA00(43,1) AA00(43,2) AA00(43,3)];      
    elseif Asmax>AA00(43,3) && Asmax<=AA00(44,3)
      Ass=[AA00(44,1) AA00(44,2) AA00(44,3)];     
    elseif Asmax>AA00(44,3) && Asmax<=AA00(45,3)
      Ass=[AA00(45,1) AA00(45,2) AA00(45,3)];       
    elseif Asmax>AA00(45,3) && Asmax<=AA00(46,3)
      Ass=[AA00(46,1) AA00(46,2) AA00(46,3)];      
    elseif Asmax>AA00(46,3) && Asmax<=AA00(47,3)
      Ass=[AA00(47,1) AA00(47,2) AA00(47,3)];      
    elseif Asmax>AA00(47,3) && Asmax<=AA00(48,3)
      Ass=[AA00(48,1) AA00(48,2) AA00(48,3)];      
    elseif Asmax>AA00(48,3) && Asmax<=AA00(49,3)
      Ass=[AA00(49,1) AA00(49,2) AA00(49,3)];       
    elseif Asmax>AA00(49,3) && Asmax<=AA00(50,3)
      Ass=[AA00(50,1) AA00(50,2) AA00(50,3)];      
    elseif Asmax>AA00(50,3) && Asmax<=AA00(51,3)
      Ass=[AA00(51,1) AA00(51,2) AA00(51,3)];
    elseif Asmax>AA00(51,3) && Asmax<=AA00(52,3)
      Ass=[AA00(52,1) AA00(52,2) AA00(52,3)];      
    elseif Asmax>AA00(52,3) && Asmax<=AA00(53,3)
      Ass=[AA00(53,1) AA00(53,2) AA00(53,3)];       
    elseif Asmax>AA00(53,3) && Asmax<=AA00(54,3)
      Ass=[AA00(54,1) AA00(54,2) AA00(54,3)];     
    elseif Asmax>AA00(54,3) && Asmax<=AA00(55,3)
      Ass=[AA00(55,1) AA00(55,2) AA00(55,3)];      
    elseif Asmax>AA00(55,3) && Asmax<=AA00(56,3)
      Ass=[AA00(56,1) AA00(56,2) AA00(56,3)];       
    elseif Asmax>AA00(56,3) && Asmax<=AA00(57,3)
      Ass=[AA00(57,1) AA00(57,2) AA00(57,3)];      
    elseif Asmax>AA00(57,3) && Asmax<=AA00(58,3)
      Ass=[AA00(58,1) AA00(58,2) AA00(58,3)];      
    elseif Asmax>AA00(58,3) && Asmax<=AA00(59,3)
      Ass=[AA00(59,1) AA00(59,2) AA00(59,3)];       
    elseif Asmax>AA00(59,3) && Asmax<=AA00(60,3)
      Ass=[AA00(60,1) AA00(60,2) AA00(60,3)];      
    elseif Asmax>AA00(60,3) && Asmax<=AA00(61,3)
      Ass=[AA00(61,1) AA00(61,2) AA00(61,3)];        
    elseif Asmax>AA00(61,3) && Asmax<=AA00(62,3)
      Ass=[AA00(62,1) AA00(62,2) AA00(62,3)];      
    elseif Asmax>AA00(62,3) && Asmax<=AA00(63,3)
      Ass=[AA00(63,1) AA00(63,2) AA00(63,3)];       
    elseif Asmax>AA00(63,3) && Asmax<=AA00(64,3)
      Ass=[AA00(64,1) AA00(64,2) AA00(64,3)];      
    elseif Asmax>AA00(64,3) && Asmax<=AA00(65,3)
      Ass=[AA00(65,1) AA00(65,2) AA00(65,3)];      
    elseif Asmax>AA00(65,3) && Asmax<=AA00(66,3)
      Ass=[AA00(66,1) AA00(66,2) AA00(66,3)];       
    elseif Asmax>AA00(66,3) && Asmax<=AA00(67,3)
      Ass=[AA00(67,1) AA00(67,2) AA00(67,3)];     
    elseif Asmax>AA00(67,3) && Asmax<=AA00(68,3)
      Ass=[AA00(68,1) AA00(68,2) AA00(68,3)];       
    elseif Asmax>AA00(68,3) && Asmax<=AA00(69,3)
      Ass=[AA00(69,1) AA00(69,2) AA00(69,3)];
    elseif Asmax>AA00(69,3) && Asmax<=AA00(70,3)
      Ass=[AA00(70,1) AA00(70,2) AA00(70,3)];      
    elseif Asmax>AA00(70,3) && Asmax<=AA00(71,3)
      Ass=[AA00(71,1) AA00(71,2) AA00(71,3)];      
    elseif Asmax>AA00(71,3) && Asmax<=AA00(72,3)
      Ass=[AA00(72,1) AA00(72,2) AA00(72,3)];       
    elseif Asmax>AA00(72,3) && Asmax<=AA00(73,3)
      Ass=[AA00(73,1) AA00(73,2) AA00(73,3)];       
    elseif Asmax>AA00(73,3) && Asmax<=AA00(74,3)
      Ass=[AA00(74,1) AA00(74,2) AA00(74,3)];        
    elseif Asmax>AA00(74,3) && Asmax<=AA00(75,3)
      Ass=[AA00(75,1) AA00(75,2) AA00(75,3)];       
    elseif Asmax>AA00(75,3) && Asmax<=AA00(76,3)
      Ass=[AA00(76,1) AA00(76,2) AA00(76,3)]; 
    elseif Asmax>AA00(76,3) && Asmax<=AA00(77,3)
      Ass=[AA00(77,1) AA00(77,2) AA00(77,3)];             

  end     
end

