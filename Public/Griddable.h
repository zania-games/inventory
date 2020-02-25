// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "UObject/Interface.h"
#include "Griddable.generated.h"

// This class does not need to be modified.
UINTERFACE(MinimalAPI)
class UGriddable: public UInterface
{
	GENERATED_BODY()
};

/**
 *
 */
class INVENTORYTEST_API IGriddable
{
	GENERATED_BODY()

	public:
		virtual int32 NumRows() const;
		virtual int32 NumColumns() const;
		virtual void OnInsert();
		virtual void OnRemove();
		virtual void OnDrop();
		// TODO: Maybe a getter for the image to be shown in the grid?
};
