// Fill out your copyright notice in the Description page of Project Settings.

#include "GridContainer.h"
#include "Griddable.h"

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Private Methods
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

bool UGridContainer::ItemFits(const IGriddable& item, int32 row, int32 col) const
{
	int32 numItemRows = item.NumRows(), numItemCols = item.NumColumns();
	int32 rowEnd = FGenericPlatformMath::Min(row + numItemRows, NumRows);
	int32 colEnd = FGenericPlatformMath::Min(col + numItemCols, NumColumns);
	for (int32 i = row; i < rowEnd; ++i)
	{
		for (int32 j = col; j < colEnd; ++j)
		{
			if (m_grid[i][j])
				return false;
		}
	}
	return true;
}

void UGridContainer::ZeroOutItem(const IGriddable& item, int32 row, int32 col)
{
	int32 numItemRows = item.NumRows(), numItemCols = item.NumColumns();
	for (int32 i = 0; i < numItemRows; ++i)
		FMemory::Memset(m_grid[row].GetData() + col, 0, numItemCols * sizeof(IGriddable*));
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Protected Methods
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Called when the game starts
void UGridContainer::BeginPlay()
{
	Super::BeginPlay();

	// ...

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Public Methods
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Sets default values for this component's properties
UGridContainer::UGridContainer()
{
	// Set this component to be initialized when the game starts, and to be ticked every frame.  You can turn these features
	// off to improve performance if you don't need them.
	PrimaryComponentTick.bCanEverTick = true;

	// ...
}

void UGridContainer::PostInitProperties()
{
	Super::PostInitProperties();
	m_grid.Init(TArray<IGriddable*>(), NumRows);
	for (TArray<IGriddable*>& row: m_grid)
		row.Init(nullptr, NumColumns);
}

int32 UGridContainer::Num() const
{
	return m_items.Num();
}

// Called every frame
void UGridContainer::TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction)
{
	Super::TickComponent(DeltaTime, TickType, ThisTickFunction);

	// ...
}

EInsertOutcome UGridContainer::Insert(IGriddable& item, int32 row, int32 col)
{
	if (m_items.Contains(&item))
		return EInsertOutcome::ALREADY_PRESENT;
	else if (!ItemFits(item, row, col))
		return EInsertOutcome::DOESNT_FIT;
	else
	{
		m_grid[row][col] = &item;
		m_items.Emplace(&item, MakeTuple(row, col));
		return EInsertOutcome::SUCCESS;
	}
}

bool UGridContainer::Remove(IGriddable& item)
{
	TTuple<int32, int32> location;
	if (m_items.RemoveAndCopyValue(&item, location))
	{
		ZeroOutItem(item, location.Get<0>(), location.Get<1>());
		return true;
	}
	else
		return false;
}

IGriddable* UGridContainer::operator()(int32 row, int32 col) const
{
	return m_grid[row][col];
}
